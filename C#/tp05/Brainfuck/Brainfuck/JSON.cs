﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;

namespace Brainfuck
{
    

    class JSONElement
    {
        public enum JSONType
        {
            DIC,
            LIST,
            STR,
            NB,
            BOOL,
            NULL
        }
        
        private JSONType type;
        
        public bool bool_value;
        public int int_value;
        public string string_value;
        public List<JSONElement> data;
        public List<string> key;

        public JSONElement(JSONType type)
        {
            this.type = type;
            if (type == JSONType.LIST || type == JSONType.DIC)
                this.data = new List<JSONElement>();
            if (type == JSONType.DIC)
                this.key = new List<string>();
        }

        public JSONType Type
        {
            get { return this.type; }
        }
        
    }

    static class JSON
    {

        public static JSONElement.JSONType GetJsonType(char c)
        {
            // TODO
            return JSONElement.JSONType.NULL;
        }

        public static string ParseString(string json, ref int index)
        {
            // TODO
            return "Not implemented";
        }


        public static int ParseInt(string json, ref int index)
        {
            // TODO
            return 0;
        }

        public static bool ParseBool(string json, ref int index)
        {
            // TODO
            return false;
        }

        public static void EatBlank(string json, ref int index)
        {
            // TODO
        }
        
        public static JSONElement ParseJSONString(string json, ref int index)
        {
            // TODO
            return null;
        }
        
        public static JSONElement ParseJSONFile(string file)
        {
            // TODO
            return null;
        }

        public static void PrintJSON(JSONElement el)
        {
            // TODO
        }
        
        public static JSONElement SearchJSON(JSONElement element, string key)
        {
            // TODO
            return null;
        }
    }   
}