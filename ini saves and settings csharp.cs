using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace networkHelper
{
    public partial class Form1 : Form
    {
        Menu menu = new Menu(true,"menu name");
        public Form1()
        {
            InitializeComponent();
            menu.initialize_setting("int1", 1);
            menu.initialize_setting("bool2", false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            menu.show();
        }

        protected override void OnPaint(PaintEventArgs e)
        { 
            base.OnPaint(e);
            menu.draw(e);
        }


    }

    public class Menu
    {
        public string settings_ini_filename = "Settings.ini";
        public menu_settings ms;

        public void show()
        {

        }
        public void hide()
        {

        }
        public void draw(PaintEventArgs e)
        {

        }

        private void Init(bool _setting_debug, string _name, Point _location)
        {
           ms.Initialize(settings_ini_filename);
           ms.initialize_setting("debug", _setting_debug);
           ms.initialize_setting("name", _name);
           ms.initialize_setting("menu_location", location.ToString());
            
            if (ms.get("debug", false))
            {
                MessageBox.Show("Debug is enabled", _name);
            }

        }


        public class menu_settings
        {
            public class Menu_item
            {
                int _index = 0;
                int parrent_id = 0;
                string name = "";
                bool hidden = false;
                bool enabled = true;
            }
            string Path;
            string EXE = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            public Dictionary<string, Menu_item> menu_layout = new Dictionary<string, Menu_item>();
            private int layout_counter = 0;

            public Dictionary<string, string> _string = new Dictionary<string, string>();
            public Dictionary<string, bool> _bool = new Dictionary<string, bool>();
            public Dictionary<string, int> _int = new Dictionary<string, int>();
            public Dictionary<string, long> _long = new Dictionary<string, long>();
            public Dictionary<string, double> _double = new Dictionary<string, double>();



            public void Initialize(string IniPath = null)
            {
                Path = new System.IO.FileInfo(IniPath ?? EXE + ".ini").FullName;
            }

            /*public menu_settings(string IniPath = null)
            {
                Path = new System.IO.FileInfo(IniPath ?? EXE + ".ini").FullName;
            }*/

            public void set(string key, bool value)
            {
                try { Write(key, value.ToString()); } catch { }
                _bool[key] = value;
            }
            public void set(string key, int value)
            {
                try { Write(key, value.ToString()); } catch { }
                _int[key] = value;
            }
            public void set(string key, string value)
            {
                try { Write(key, value.ToString()); } catch { }
                _string[key] = value;
            }
            public void set(string key, long value)
            {
                try { Write(key, value.ToString()); } catch { }
                _long[key] = value;
            }
            public void set(string key, double value)
            {
                try { Write(key, value.ToString()); } catch { }
                _double[key] = value;
            }

            public bool get(string key,bool b = false)
            {
                try
                {
                    return _bool[key];
                }
                catch { }
                return b;
            }
            public int get(string key, int i = 0)
            {
                try
                {
                    return _int[key];
                }
                catch { }
                return i;
            }
            public string get(string key, string s = "")
            {
                try
                {
                    return _string[key];
                }
                catch { }
                return s;
            }
            public long get(string key, long l = 0)
            {
                try
                {
                    return _long[key];
                }
                catch { }
                return l;
            }
            public double get(string key, double d = 0.0)
            {
                try
                {
                    return _double[key];
                }
                catch { }
                return d;
            }

            public void initialize_setting(string key, string value)
            {
                _string[key] = _get_s(key, value);
              
                layout_counter++;
            }
            public void initialize_setting(string key, bool value)
            {
                _bool[key] = _get_b(key, value);
                layout_counter++;
            }
            public void initialize_setting(string key, int value)
            {
                _int[key] = _get_i(key, value);
                layout_counter++;
            }
            public void initialize_setting(string key, long value)
            {
                _long[key] = _get_l(key, value);
                layout_counter++;
            }
            public void initialize_setting(string key, double value)
            {
                _double[key] = _get_d(key, value);
                layout_counter++;
            }

            public string _get_s(string _key, string default_value = "")
            {
                try
                {
                    return Read(_key);
                }
                catch
                {
                    try { Write(_key, default_value); } catch { }
                }
                return default_value;
            }
            public bool _get_b(string _key, bool default_value = false)
            {
                try
                {
                    return bool.Parse(Read(_key));
                }
                catch
                {
                    try { Write(_key, default_value.ToString()); } catch { }
                }
                return default_value;
            }
            public int _get_i(string _key, int default_value = 0)
            {
                try
                {
                    return int.Parse(Read(_key));
                }
                catch
                {
                    try { Write(_key, default_value.ToString()); } catch { }
                }
                return default_value;
            }
            public long _get_l(string _key, long default_value = 0)
            {
                try
                {
                    return long.Parse(Read(_key));
                }
                catch
                {
                    try { Write(_key, default_value.ToString()); } catch { }
                }
                return default_value;
            }
            public double _get_d(string _key, double default_value = 0.0)
            {
                try
                {
                    return long.Parse(Read(_key));
                }
                catch
                {
                    try { Write(_key, default_value.ToString()); } catch { }
                }
                return default_value;
            }

            [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [System.Runtime.InteropServices.DllImport("kernel32", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
       }
