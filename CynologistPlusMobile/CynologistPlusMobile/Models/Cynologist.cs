using System;
using System.Collections.Generic;

namespace CynologistPlusMobile.Model
{
    public class Cynologist
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AuthCredentialId { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual DogTrainingCenter DogTrainingCenter { get; set; }
    }

}

