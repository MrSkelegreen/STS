using Microsoft.EntityFrameworkCore;
using STS.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS
{
    class UserService
    {
        private readonly STSContext DbContext;

        public UserService(STSContext DbContext)
        {
            this.DbContext = DbContext;
        }

    }
}
