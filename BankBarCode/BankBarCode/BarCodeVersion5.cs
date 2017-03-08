namespace BankBarCode
{
    /// <summary>
    /// International bank barcode
    /// </summary>
    class BarCodeVersion5 : BarCode
    {

        /// <summary>
        /// set reference number
        /// </summary>
        /// <param name="reference"> reference number (in domestic format) </param>
        public override void SetReference(string reference)
        {
            // convert reference number from domestic to international format
            reference = ReferenceChecker.ConvertToISO11649(reference);

            reference = reference.Substring(2);

            while (reference.Length < 23)
            {
                // fill leading zeros
                reference = reference.Insert(2, "0");
            }
            this._reference = reference;
        }

        /// <summary>
        /// get bank barcode (international format)
        /// </summary>
        /// <returns> bank bar code </returns>
        protected override string GetBarCodeWithoutChecksum()
        {
            return $"5{_account}{_euros:D6}{_cents:D2}{_reference}{_duedate}";
        }

    }
}