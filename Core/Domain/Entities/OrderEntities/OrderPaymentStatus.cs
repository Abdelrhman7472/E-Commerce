using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntities
{
    public enum OrderPaymentStatus //Zy el Addres haykon fe nafs el table bta3 order fel Db (Zy fekret Complex Attributes in SQL)
    {
        Pending =0,
        PaymentReceived =1,
        PaymentFailed=2
    }
}
