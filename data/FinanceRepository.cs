using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.data
{
    public class FinanceRepository
    {
        private readonly FinanceContext context_;

        public FinanceRepository(FinanceContext context)
        {
            context_ = context;        
        }
    }
}
