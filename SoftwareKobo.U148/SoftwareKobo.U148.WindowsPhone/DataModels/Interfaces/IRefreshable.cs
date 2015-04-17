using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareKobo.U148.DataModels.Interfaces
{
    public interface IRefreshable
    {
        Task Refresh();
    }
}
