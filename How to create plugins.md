# Making plugins
### Requirements
* Microsoft Visual Studio (C#)
* .Net Framework 4.5.2
* Source Material Manager

### Let's make plugin!
1. Create project using template _Dynamic link library (C#)_
2. Browse to _Source Material Manager_ directory and add file _"SMM.exe"_ to references
3. Remove default class and create new one with name _Addon_
4. Paste code bellow:
```c#
using SMM;

namespace YOURNAMESPACE
{
    public class Addon : IAddon
    {
        public AddonInfo Info
        {
            get
            {
                return new AddonInfo() {
                    Name = "YOURPLUGINNAME",
                    Publisher = "YOURNAME",
                    Version = "1.0.0"
                };
            }
        }

        public void Initialize()
        {
            //Insert code here
        }

        public void Shutdown()
        {
            //Insert code here
        }
    }
}
```

### Finishing out code
Replace:
* _YOURNAMESPACE_ with your namespace
* _YOURPLUGINNAME_ with name of your plugin
* _YOURNAME_ with your name, nickname, etc.
* _"//Insert code here"_ with code you want to execute

### About most important methods:
* medhod _Initialize()_ is being executed when _SMM_ is launching
* method _Shutdown()_ is being executed when _SMM_ is shutting down
