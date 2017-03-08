namespace BankBarCode
{
    /// <summary>
    /// Domestic bank barcode
    /// </summary>
    class BarCodeVersion4 : BarCode
    {

        /// <summary>
        /// set reference number
        /// </summary>
        /// <param name="reference"> reference number (in domestic format)</param>
        public override void SetReference(string reference)
        {
            while (reference.Length < 20)
            {
                // fill leading zeros
                reference = reference.Insert(0, "0");
            }
            this._reference = reference;
        }

        /// <summary>
        /// get bank barcode (domestic format)
        /// </summary>
        /// <returns> bank barcode </returns>
        protected override string GetBarCodeWithoutChecksum()
        {
            return $"4{_account}{_euros:D6}{_cents:D2}000{_reference}{_duedate}";
        }

    }
}
