namespace LINQKonzolnaAplikacija
{
    internal class Student
    {
        public string jmbag;
        public string v;
        public Gender Gender { get; set; }

        public Student(string v, string jmbag)
        {
            this.v = v;
            this.jmbag = jmbag;
        }

        protected bool Equals(Student other)
        {
            return string.Equals(jmbag, other.jmbag);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Student) obj);
        }

        public override int GetHashCode()
        {
            return (jmbag != null ? jmbag.GetHashCode() : 0);
        }

        public static bool operator ==(Student a, Student b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.jmbag.Equals(b.jmbag);
        }

        public static bool operator !=(Student a, Student b)
        {
            return !a.jmbag.Equals(b.jmbag);
        }
    }

    public enum Gender
    {
        Male, Female
    }
}