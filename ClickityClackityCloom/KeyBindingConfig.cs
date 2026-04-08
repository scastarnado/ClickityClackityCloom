using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ClickityClacityCloom
{
    [Serializable]
    public class KeyBinding
    {
        public string ActionName { get; set; }
        public string ActionDescription { get; set; }
        public Keys Key { get; set; }
        
        [JsonIgnore]
        public string KeyDisplayName
        {
            get { return Key.ToString(); }
        }
    }

    public class KeyBindingConfiguration
    {
        public List<KeyBinding> Bindings { get; set; }

        public KeyBindingConfiguration()
        {
            Bindings = new List<KeyBinding>();
        }

        public static KeyBindingConfiguration CreateDefault()
        {
            var config = new KeyBindingConfiguration();
            config.Bindings.Add(new KeyBinding 
            { 
                ActionName = "EmergencyStop", 
                ActionDescription = "Emergency Stop All",
                Key = Keys.Escape 
            });
            config.Bindings.Add(new KeyBinding 
            { 
                ActionName = "StartClicking", 
                ActionDescription = "Start Clicking",
                Key = Keys.F6 
            });
            config.Bindings.Add(new KeyBinding 
            { 
                ActionName = "StopClicking", 
                ActionDescription = "Stop Clicking",
                Key = Keys.F7 
            });
            config.Bindings.Add(new KeyBinding 
            { 
                ActionName = "StartMacro", 
                ActionDescription = "Start Macro Playback",
                Key = Keys.F8 
            });
            config.Bindings.Add(new KeyBinding 
            { 
                ActionName = "StopMacro", 
                ActionDescription = "Stop Macro Playback",
                Key = Keys.F9 
            });
            return config;
        }

        public void Save(string filePath)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save keybindings: {ex.Message}", ex);
            }
        }

        public static KeyBindingConfiguration Load(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    var defaultConfig = CreateDefault();
                    defaultConfig.Save(filePath);
                    return defaultConfig;
                }

                string json = File.ReadAllText(filePath);
                var config = JsonConvert.DeserializeObject<KeyBindingConfiguration>(json);
                
                if (config == null || config.Bindings == null || config.Bindings.Count == 0)
                {
                    var defaultConfig = CreateDefault();
                    defaultConfig.Save(filePath);
                    return defaultConfig;
                }

                return config;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load keybindings: {ex.Message}", ex);
            }
        }

        public Keys GetKeyForAction(string actionName)
        {
            var binding = Bindings.FirstOrDefault(b => b.ActionName == actionName);
            return binding?.Key ?? Keys.None;
        }

        public bool HasConflict(string actionName, Keys newKey)
        {
            return Bindings.Any(b => b.ActionName != actionName && b.Key == newKey);
        }

        public string GetConflictingAction(string actionName, Keys newKey)
        {
            var conflict = Bindings.FirstOrDefault(b => b.ActionName != actionName && b.Key == newKey);
            return conflict?.ActionDescription;
        }
    }
}
