using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace usersBugredRu.Helpers
{
    class Helper
    {
        public string DateTimeNowString = DateTime.Now.ToString("ddMMyyyyhhmmss");

        public bool CheckResponseForAllCompanyUsers(List<string> companyUsersRequest, string companyUsersResponse)
        {
            foreach (string user in companyUsersRequest)
            {
                if (!companyUsersResponse.Contains(user))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
