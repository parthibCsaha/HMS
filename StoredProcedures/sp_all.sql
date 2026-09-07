-- =====================================================
-- HMS Stored Procedures for PostgreSQL
-- Run these in your hms_db database
-- =====================================================

-- =====================================================
-- 1. sp_get_next_code: Auto-increment code generator
-- Used by CodeGeneratorService for PAT-00001, DOC-00001, etc.
-- =====================================================
CREATE OR REPLACE FUNCTION sp_get_next_code(p_prefix TEXT)
RETURNS TEXT AS $$
DECLARE
    v_next_val INT;
    v_code TEXT;
BEGIN
    -- Create sequence table if not exists
    CREATE TABLE IF NOT EXISTS code_sequences (
        prefix VARCHAR(10) PRIMARY KEY,
        last_value INT NOT NULL DEFAULT 0
    );

    -- Upsert and get next value
    INSERT INTO code_sequences (prefix, last_value)
    VALUES (p_prefix, 1)
    ON CONFLICT (prefix) DO UPDATE SET last_value = code_sequences.last_value + 1
    RETURNING last_value INTO v_next_val;

    v_code := p_prefix || '-' || LPAD(v_next_val::TEXT, 5, '0');
    RETURN v_code;
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- 2. sp_get_dashboard_stats: Overall hospital stats
-- =====================================================
CREATE OR REPLACE FUNCTION sp_get_dashboard_stats()
RETURNS TABLE (
    total_patients BIGINT,
    total_doctors BIGINT,
    total_appointments_today BIGINT,
    active_admissions BIGINT,
    available_beds BIGINT,
    revenue_this_month NUMERIC,
    revenue_last_month NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        (SELECT COUNT(*) FROM patients WHERE is_deleted = false)::BIGINT,
        (SELECT COUNT(*) FROM doctors WHERE is_deleted = false)::BIGINT,
        (SELECT COUNT(*) FROM appointments WHERE appointment_date = CURRENT_DATE AND is_deleted = false)::BIGINT,
        (SELECT COUNT(*) FROM admission_records WHERE is_active = true AND is_deleted = false)::BIGINT,
        (SELECT COUNT(*) FROM beds WHERE status = 0 AND is_deleted = false)::BIGINT, -- 0 = Available
        (SELECT COALESCE(SUM(paid_amount), 0) FROM invoices
            WHERE invoice_date >= DATE_TRUNC('month', CURRENT_DATE) AND is_deleted = false),
        (SELECT COALESCE(SUM(paid_amount), 0) FROM invoices
            WHERE invoice_date >= DATE_TRUNC('month', CURRENT_DATE - INTERVAL '1 month')
            AND invoice_date < DATE_TRUNC('month', CURRENT_DATE) AND is_deleted = false);
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- 3. sp_get_occupancy_report: Ward occupancy breakdown
-- =====================================================
CREATE OR REPLACE FUNCTION sp_get_occupancy_report()
RETURNS TABLE (
    ward_id UUID,
    ward_name TEXT,
    total_beds INT,
    occupied_beds BIGINT,
    available_beds BIGINT,
    occupancy_rate NUMERIC
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        w.id,
        w.name::TEXT,
        w.total_beds,
        (SELECT COUNT(*) FROM beds b WHERE b.ward_id = w.id AND b.status = 1 AND b.is_deleted = false)::BIGINT,
        (SELECT COUNT(*) FROM beds b WHERE b.ward_id = w.id AND b.status = 0 AND b.is_deleted = false)::BIGINT,
        CASE WHEN w.total_beds > 0
            THEN ROUND(
                (SELECT COUNT(*) FROM beds b WHERE b.ward_id = w.id AND b.status = 1 AND b.is_deleted = false)::NUMERIC
                / w.total_beds * 100, 2)
            ELSE 0
        END
    FROM wards w
    WHERE w.is_deleted = false AND w.is_active = true
    ORDER BY w.name;
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- 4. sp_get_appointment_trends: Daily appointment counts
-- =====================================================
CREATE OR REPLACE FUNCTION sp_get_appointment_trends(p_from DATE, p_to DATE)
RETURNS TABLE (
    appointment_date DATE,
    total_count BIGINT,
    completed_count BIGINT,
    cancelled_count BIGINT,
    no_show_count BIGINT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        d.dt::DATE,
        (SELECT COUNT(*) FROM appointments a WHERE a.appointment_date = d.dt AND a.is_deleted = false)::BIGINT,
        (SELECT COUNT(*) FROM appointments a WHERE a.appointment_date = d.dt AND a.status = 4 AND a.is_deleted = false)::BIGINT, -- Completed
        (SELECT COUNT(*) FROM appointments a WHERE a.appointment_date = d.dt AND a.status = 5 AND a.is_deleted = false)::BIGINT, -- Cancelled
        (SELECT COUNT(*) FROM appointments a WHERE a.appointment_date = d.dt AND a.status = 6 AND a.is_deleted = false)::BIGINT  -- NoShow
    FROM generate_series(p_from, p_to, INTERVAL '1 day') AS d(dt)
    ORDER BY d.dt;
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- 5. sp_get_revenue_report: Monthly revenue summary
-- =====================================================
CREATE OR REPLACE FUNCTION sp_get_revenue_report(p_from DATE, p_to DATE)
RETURNS TABLE (
    month_year TEXT,
    total_invoiced NUMERIC,
    total_paid NUMERIC,
    total_outstanding NUMERIC,
    invoice_count BIGINT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        TO_CHAR(DATE_TRUNC('month', i.invoice_date), 'YYYY-MM')::TEXT,
        COALESCE(SUM(i.total_amount), 0),
        COALESCE(SUM(i.paid_amount), 0),
        COALESCE(SUM(i.balance_amount), 0),
        COUNT(*)::BIGINT
    FROM invoices i
    WHERE i.invoice_date BETWEEN p_from AND p_to AND i.is_deleted = false
    GROUP BY DATE_TRUNC('month', i.invoice_date)
    ORDER BY DATE_TRUNC('month', i.invoice_date);
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- 6. sp_get_patient_demographics: Age, gender, blood group
-- Returns 3 result sets via multi-query
-- =====================================================
CREATE OR REPLACE FUNCTION sp_get_patient_demographics()
RETURNS TABLE (
    group_name TEXT,
    group_type TEXT,
    count BIGINT
) AS $$
BEGIN
    -- Age groups
    RETURN QUERY
    SELECT
        CASE
            WHEN EXTRACT(YEAR FROM AGE(p.date_of_birth)) < 18 THEN '0-17'
            WHEN EXTRACT(YEAR FROM AGE(p.date_of_birth)) BETWEEN 18 AND 30 THEN '18-30'
            WHEN EXTRACT(YEAR FROM AGE(p.date_of_birth)) BETWEEN 31 AND 45 THEN '31-45'
            WHEN EXTRACT(YEAR FROM AGE(p.date_of_birth)) BETWEEN 46 AND 60 THEN '46-60'
            ELSE '60+'
        END::TEXT,
        'AgeGroup'::TEXT,
        COUNT(*)::BIGINT
    FROM patients p
    WHERE p.is_deleted = false
    GROUP BY 1;

    -- Gender distribution
    RETURN QUERY
    SELECT
        p.gender::TEXT,
        'Gender'::TEXT,
        COUNT(*)::BIGINT
    FROM patients p
    WHERE p.is_deleted = false
    GROUP BY p.gender;

    -- Blood group distribution
    RETURN QUERY
    SELECT
        p.blood_group::TEXT,
        'BloodGroup'::TEXT,
        COUNT(*)::BIGINT
    FROM patients p
    WHERE p.is_deleted = false
    GROUP BY p.blood_group;
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- Done! All stored procedures created.
-- =====================================================
