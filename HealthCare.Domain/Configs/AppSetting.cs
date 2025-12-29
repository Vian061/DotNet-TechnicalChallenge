using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCare.Domain.Configs
{

    public class AppSetting
    {
        public AppConfig AppConfig { get; set; } = default!;
        public Logging Logging { get; set; } = default!;
        public string AllowedHosts { get; set; } = default!;
    }

    public class AppConfig
    {
        public int CutOffHour { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; } = default!;
    }

    public class Loglevel
    {
        public string Default { get; set; } = default!;
        public string MicrosoftAspNetCore { get; set; } = default!;
    }

}
