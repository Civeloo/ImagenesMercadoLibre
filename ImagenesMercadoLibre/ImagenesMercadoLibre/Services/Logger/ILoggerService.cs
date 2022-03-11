using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImagenesMercadoLibre.Services.Logger
{
    public interface ILoggerService
    {
        void Ex(Exception ex);
    }
}
