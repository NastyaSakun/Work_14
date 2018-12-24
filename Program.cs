using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Work14
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventar skameika = new Skameika();            

            string a = "Bed";
            Console.WriteLine($"Полиморфизм (умножение длин строк):\0");
            for(int i=0;i<3;i++)
            Console.WriteLine($"\0{skameika.V_method(a.Length)}");
            Console.WriteLine();

            Console.WriteLine("\tРабота с сериализацией/десериализацией");
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (FileStream fs = new FileStream("F:\\Skameika.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, skameika);
                    Console.WriteLine("\0Объект сериализован!");
                }

                using (FileStream fs = new FileStream("F:\\Skameika.dat", FileMode.OpenOrCreate))
                {
                    Inventar newSkameika = (Inventar)formatter.Deserialize(fs);

                    Console.Write("\0Объект десериализован!\0");
                    Console.Write($"{skameika.V_method(a.Length)}\n");
                }
            }

            catch(System.Runtime.Serialization.SerializationException e)
            {
                Console.WriteLine(e.Message);
            }

            SoapFormatter formatter2 = new SoapFormatter();

            using (FileStream fs2 = new FileStream("F:\\Skameika.soap", FileMode.OpenOrCreate))
            {
                formatter2.Serialize(fs2, skameika);
                Console.WriteLine("\0Объект сериализован!");
            }

            using (FileStream fs2 = new FileStream("F:\\Skameika.soap", FileMode.OpenOrCreate))
            {
                Inventar newSkameika = (Inventar)formatter2.Deserialize(fs2);
                Console.Write("\0Объект десериализован!\0");
                Console.Write($"{skameika.V_method(a.Length)}\n");
            }


            Skameika skameika3 = new Skameika();

            XmlSerializer formatter3 = new XmlSerializer(typeof(Skameika));

            using (FileStream fs3 = new FileStream("F:\\Skameika.xml", FileMode.OpenOrCreate))
            {
                formatter3.Serialize(fs3, skameika3);
                Console.WriteLine("\0Объект сериализован!");
            }

            using (FileStream fs3 = new FileStream("F:\\Skameika.xml", FileMode.OpenOrCreate))
            {
                Skameika newSkameika=(Skameika)formatter3.Deserialize(fs3);
                Console.Write("\0Объект десериализован!\0");
                Console.Write($"{skameika.V_method(a.Length)}\n");
            }

            Skameika skameika4 = new Skameika();

            DataContractJsonSerializer jFormatter = new DataContractJsonSerializer(typeof(Skameika));

            using (FileStream fs4 = new FileStream("F:\\Skameika.json", FileMode.OpenOrCreate))
            {
                jFormatter.WriteObject(fs4, skameika4);
                Console.WriteLine("\0Объект сереализован!");
            }

            using (FileStream fs = new FileStream("F:\\Skameika.json", FileMode.OpenOrCreate))
            {
                Skameika newSkameika= (Skameika)jFormatter.ReadObject(fs);
                Console.Write("\0Объект десериализован!\0");
                Console.Write($"{skameika.V_method(a.Length)}\n");
            }

            Console.WriteLine("\n\tРабота с массивом объектов:");
            ForArray one = new ForArray(2);
            ForArray two = new ForArray(4);
            ForArray[] objects = { one, two };

            BinaryFormatter binary = new BinaryFormatter();

            using (FileStream fsBin = new FileStream("F:\\ForArray.dat", FileMode.OpenOrCreate))
            {
                binary.Serialize(fsBin, objects);
                Console.WriteLine("\0Объект сериализован!");
            }
            using (FileStream fsBin = new FileStream("F:\\ForArray.dat", FileMode.OpenOrCreate))
            {
                ForArray[] Teches2 = (ForArray[])binary.Deserialize(fsBin);
                Console.WriteLine("\0Объект десериализован!");
                foreach (ForArray t in Teches2)
                {
                    Console.WriteLine($"\0число -{t.number}");
                }
            }
            

            Console.WriteLine("\n\tРабота с xml:");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("F:\\Skameika.xml");
            XmlElement xRoot = xDoc.DocumentElement;

            Console.Write("\0Вывод дочерних элементов:\0");
            XmlNodeList childnodes = xRoot.SelectNodes("*");
            foreach (XmlNode n in childnodes)
                Console.Write($"{n.OuterXml}\0");
            Console.WriteLine();

            Console.Write("\0Вывод по имени:\0");
            XmlNodeList onName = xRoot.SelectNodes("one");
            foreach (XmlNode n in onName)
                Console.Write($"{n.OuterXml}\0");


            Console.WriteLine("\n\n\tРабота с linq to xml:");
            XDocument xdoc = new XDocument();
            XElement universityBSTU = new XElement("University");
            XAttribute nameBSTU = new XAttribute("name", "BSTU");
            XElement facultyBSTU = new XElement("Faculty", "IT");
            universityBSTU.Add(nameBSTU);
            universityBSTU.Add(facultyBSTU);

            XElement universityBSUIR = new XElement("University");
            XAttribute nameBSUIR = new XAttribute("name", "BSUIR");
            XElement facultyBSUIR = new XElement("Faculty", "ASOI");
            universityBSUIR.Add(nameBSUIR);
            universityBSUIR.Add(facultyBSUIR);
            
            XElement universities = new XElement("universities");
           
            universities.Add(universityBSTU);
            universities.Add(universityBSUIR);
            
            xdoc.Add(universities);
            
            xdoc.Save("F:\\Universities.xml");

            Console.WriteLine("\0Создан документ F:\\Universities.xml");

            Console.Write("\0Запрашиваем университет по специальности:\0");
            XDocument xdoc_ = XDocument.Load("F:\\Universities.xml");
            var items = from xe in xdoc.Element("universities").Elements("University")
                        where xe.Element("Faculty").Value == "ASOI"
                        select new Universities
                        {
                            Name = xe.Attribute("name").Value,
                        };

            foreach (var item in items)
                Console.WriteLine("{0}", item.Name);
        }
    }
        
}
