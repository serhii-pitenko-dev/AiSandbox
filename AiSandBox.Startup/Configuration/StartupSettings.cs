using AiSandBox.SharedBaseTypes.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSandBox.Startup.Configuration;

public class StartupSettings
{
    public PresentationType PresentationType { get; set; }

    public bool TestPreconditionsEnabled { get; set; }

    public bool IsWebApiEnabled { get; set; }
}

