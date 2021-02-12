using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.DAL.Abstract
{
    public interface IWorker : IDisposable
    {
       
        void SaveChanges();
    }
}
