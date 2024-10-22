using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundException(int id) :NotFoundException($"Can't Find DeliveryMethod with Id {id}")
    {
    }
}
