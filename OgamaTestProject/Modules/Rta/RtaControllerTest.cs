﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ogama.Modules.Rta;
using Ogama.Modules.Rta.RtaReplay;
using Ogama.Modules.Rta.Model;

namespace OgamaTestProject.Modules.Rta
{
    [TestClass]
    public class RtaControllerTest
    {
        [TestMethod]
        public void TestgetAvailbleVideoFilterNames()
        {
            RtaController cut = new RtaController();

            List<string> list = cut.getAvailbleVideoFilterNames();

            Assert.IsNotNull(list);
            for (int i = 0; i < list.Count; i++)
            {
                string filtername = list[i];
                Log("available filtername:" + filtername);
                Console.WriteLine("filtername:" + filtername);
            }

        }

        protected void Log(string s)
        {
            System.IO.StreamWriter sw = System.IO.File.AppendText(
                "c:/testog.txt");
            sw.WriteLine(s);
            sw.Close();
        }

        [TestMethod]
        public void TestSetup()
        {
            RtaController cut = new RtaController();
            RtaSettings rtaSettings = getRtaSettings();
            cut.setup(rtaSettings);

            cut.start();
            System.Threading.Thread.Sleep(1000 * 5);
            cut.stop();


        }

        public RtaSettings getRtaSettings()
        {
            RtaSettings settings = new RtaSettings();
            settings.MonitorIndex = 0;
            settings.Framerate = 20;
            //settings.VideoCompressorName = "ffdshow video encoder";
            settings.VideoCompressorName = "Xvid MPEG-4 Codec";
            settings.TempFilename = "c:/temp/RtaControllerTest01Temp.avi";
            settings.Filename = "c:/temp/RtaControllerTest01.avi";
            settings.AudioInputDeviceName = "Creative Sound Blaster-PCI";
            settings.AudioCompressorName = "PCM";
            return settings;
        }


    }


}
