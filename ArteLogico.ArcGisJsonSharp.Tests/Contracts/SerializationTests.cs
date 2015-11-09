﻿using ArteLogico.ArcGisJsonSharp.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApprovalTests;
using ApprovalTests.Reporters;

namespace ArteLogico.ArcGisJsonSharp.Tests.Contracts
{
    [TestFixture]
    [UseReporter(typeof(DiffReporter))]
    public class SerializationTests
    {
        [Test]
        public void SingleBasemapLayerRoundTripSerialization()
        {
            // Based on a sample given by ArcGIS on
            // http://resources.arcgis.com/en/help/arcgis-web-map-json/index.html#/Single_basemap_layer/02qt00000016000000/
            string expected = File.ReadAllText(@"Contracts\SingleBasemapLayer.json");
            var serializer = new JsonSerializer()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };

            string result;
            using (var reader = new StringReader(expected))
            using (var jsonReader = new JsonTextReader(reader))
            using (var writer = new StringWriter())
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.Formatting = Formatting.Indented;
                var webMap = serializer.Deserialize<WebMap>(jsonReader);
                serializer.Serialize(jsonWriter, webMap);
                result = writer.ToString();
            }

            Approvals.Verify(result);
        }
    }
}
