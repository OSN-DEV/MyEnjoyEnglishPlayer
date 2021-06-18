using System;
using System.Collections.Generic;

namespace MyEnjoyEnglishPlayer.Data {
    // https://dobon.net/vb/dotnet/file/xmlserializerhashtable.html
    [Serializable]
    public struct KeyPair<T1, T2> {
        public T1 Key;
        public T2 Value;

        public KeyPair(KeyValuePair<T1, T2> pair) {
            Key = pair.Key;
            Value = pair.Value;
        }

        public static Dictionary<T1, T2> ToDictionary(List<KeyPair<T1, T2>> list) {
            var dic = new Dictionary<T1, T2>();

            foreach(var pair in list) {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

        public static List<KeyPair<T1, T2>> ToList(Dictionary<T1, T2> dic) {
            var list = new List<KeyPair<T1, T2>>();
            foreach(var pair in dic) {
                list.Add(new KeyPair<T1, T2>(pair));
            }
            return list;
        }
    }
}
