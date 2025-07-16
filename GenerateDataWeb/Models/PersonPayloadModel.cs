namespace GenerateDataWeb.Models
{
    using System.Collections.Generic;

    namespace GenerateDataWeb.Models
    {
        public class PersonWrapper
        {
            public string Name { get; set; }  
            public int Age { get; set; }      
            public string Email { get; set; } 
            public List<PersonPayloadModel> Payload { get; set; }
        }

        public class PersonPayloadModel
        {
            public int Id { get; set; }
            public string Nama { get; set; }
            public int GenderId { get; set; }
            public string GenderName { get; set; }
            public string HobiName { get; set; }
            public int Age { get; set; }
        }
    }

}
