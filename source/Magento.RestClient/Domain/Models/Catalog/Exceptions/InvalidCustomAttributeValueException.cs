using System;
using System.Collections.Generic;
using Magento.RestClient.Data.Models.EAV.Attributes;

namespace Magento.RestClient.Domain.Models.Catalog.Exceptions
{
	public class InvalidCustomAttributeValueException : Exception
	{
		public string AttributeCode { get; }
		public List<Option> ValidOptions { get; }
		public string InputValue { get; }

		public InvalidCustomAttributeValueException(string attributeCode, List<Option> attributeOptions, object inputValue) :
			base(
				$"The assigned value of {inputValue} is not valid for {attributeCode}."
			)
		{
			this.AttributeCode = attributeCode;
			this.ValidOptions = attributeOptions;
			this.InputValue = inputValue.ToString();
		}

		public InvalidCustomAttributeValueException() : base()
		{
		}

		public InvalidCustomAttributeValueException(string message) : base(message)
		{
		}

		public InvalidCustomAttributeValueException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}