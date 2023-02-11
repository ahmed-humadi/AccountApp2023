using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWorkLib
{
    public enum ErrorType
    {
        AccountEmpty = 0,
        StoreEmpty = 1,
        BillTotalEmpty = 2,
        BillCollectionEmpty = 3,
        BillPaymentTypeEmpty = 4,

        CashDayTotalEmpty = 6,
        CashDayAccountEmpty = 7,
        CashDayCollectionEmpty = 8,

        NoError = 9,
    }
}
