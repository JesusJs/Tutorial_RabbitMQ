using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitApp.Send
{
    public interface ISend
    {
        Task ConnectionAsync();
    }
}
