using System.Collections.Generic;
using System.Linq;
using Q42.HueApi;

namespace TeamFlash.Hue
{
    class HueBuildLight : BuildLightBase, IBuildLight
    {
        private readonly HueClient _hueClient;
        private readonly List<string> _lights;
        private readonly object _lockObject = new object();

        public static string AppKey
        {
            get { return "beatsadelcomanyday"; }
        }

        public static string AppName
        {
            get { return "TeamFlash"; }
        }

        public HueBuildLight(string ip,IEnumerable<string> lights)
        {
            _hueClient = new HueClient(ip);
            _hueClient.Initialize(AppKey);
            _lights = lights.ToList();
        }

        private void SetColour(int hue, int sat = 255)
        {
            var command = new LightCommand();
            command.TurnOn();
            command.Saturation = sat;
            command.Brightness = 255;
            command.Hue = hue;
            //command.Alert = Alert.Once;
            command.Effect = Effect.None;
            _hueClient.SendCommandAsync(command, _lights).Wait();
        }

        protected override void ChangeColor(LightColour colour)
        {
            lock (_lockObject)
            {
                switch (colour)
                {
                    case LightColour.Red:
                        CurrentColour = LightColour.Red;
                        SetColour(65280);
                        break;
                    case LightColour.Green:
                        CurrentColour = LightColour.Green;
                        SetColour(25500);
                        break;
                    case LightColour.Blue:
                        CurrentColour = LightColour.Blue;
                        SetColour(46920);
                        break;
                    case LightColour.Yellow:
                        CurrentColour = LightColour.Yellow;
                        SetColour(12750);
                        break;
                    case LightColour.White:
                        CurrentColour = LightColour.White;
                        SetColour(12750, 0);
                        break;
                    case LightColour.Purple:
                        CurrentColour = LightColour.Purple;
                        SetColour(56100);
                        break;
                    case LightColour.Off:
                        CurrentColour = LightColour.Off;
                        var command = new LightCommand();
                        command.TurnOff();
                        command.Effect = Effect.None;
                        _hueClient.SendCommandAsync(command, _lights);
                        break;
                }
            }
        }
    }
}
