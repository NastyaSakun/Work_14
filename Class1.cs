using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work14
{
    class Universities
    {
        public string Name { get; set; }
    }

    [Serializable]
    [DataContract]
    public abstract class Inventar : Interface1
    {
        public string one = "Inventar";

        virtual public int V_method(int a)
        {
            return (one.Length + a);
        }

        public int Summ(int a, int b)
        {
            return a + b;
        }

        public string Summ(string a)
        {
            return a + one;
        }
    }

    [Serializable]
    [DataContract]
    public class Skameika : Inventar
    {
        [DataMember]
        public string Name { get; set; }

        public Skameika()
        {

        }

        public override int V_method(int a)
        {
            return (one.Length * a);
        }

        public int E = 28;
        public override string ToString()
        {
            return ($"\0Информация об объекте:\0{E},\0{E.Equals(6)},\0{E.GetHashCode()},\0{E.GetType()}");
        }
    }

    [Serializable]
    class ForArray
    {
        public int number;

        public ForArray(int one)
        {
            number = one++;
        }
    }
}
