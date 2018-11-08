﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outreach.Data.Interface
{
   public interface IRepository <T>
    {
        IEnumerable<T> GetAll();
        void Create(T t);
        void Update(T t);
    }
}
