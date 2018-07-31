using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.ContactAPI.Models.Dtos
{
    public class AppSetting
    {
        public string MongoConnectionString { get; set; }

        public string ContactDataBaseName { get; set; }


    }
}
