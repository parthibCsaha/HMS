using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Domain.Enums
{
    public enum PaymentMethod
    {
        Cash = 1, CreditCard = 2, DebitCard = 3,
        Insurance = 4, OnlineTransfer = 5, Cheque = 6, MobileWallet = 7
    }
}
