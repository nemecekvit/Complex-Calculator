namespace Complex_Number_Calculator_GUI
{
    public class memoryManager
    {
        private string memory1 = string.Empty;
        private string memory2 = string.Empty;
        private string memory3 = string.Empty;

        public void Save(int slot, string value)
        {
            switch (slot)
            {
                case 1: memory1 = value; break;
                case 2: memory2 = value; break;
                case 3: memory3 = value; break;
            }
        }

        public string Load(int slot)
        {
            return slot switch
            {
                1 => memory1,
                2 => memory2,
                3 => memory3,
                _ => string.Empty,
            };
        }

        public void Clear(int slot)
        {
            switch (slot)
            {
                case 1: memory1 = string.Empty; break;
                case 2: memory2 = string.Empty; break;
                case 3: memory3 = string.Empty; break;
            }
        }
    }
}
