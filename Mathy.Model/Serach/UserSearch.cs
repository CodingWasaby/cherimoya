using System;

namespace Mathy.Model.Serach
{
    public class UserSearch
    {
        public string UserName { get; set; }

        public string Company { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
