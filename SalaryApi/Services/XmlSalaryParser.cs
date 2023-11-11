using SalaryApi.DTO.Service;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace SalaryApi.Services
{
    
    public class XmlSalaryParser : ISalaryDataParser
    {
        private string _data;
        public XmlSalaryParser(string data)
        {
            _data = data;
        }
        public List<SalaryData> Parse()
        {
            //XmlSerializer deserializer = new XmlSerializer(typeof(SalaryList) );

            //byte[] byteArray = Encoding.UTF8.GetBytes(_data);

            //MemoryStream stream = new MemoryStream(byteArray);

            //var salaryData = (SalaryList) deserializer.Deserialize(stream) ;

            //return null;
            throw new NotImplementedException();
        }


    }

    public class SalaryList
    {
        public List<SalaryData> Salaries{ get; set; }
    }
}
