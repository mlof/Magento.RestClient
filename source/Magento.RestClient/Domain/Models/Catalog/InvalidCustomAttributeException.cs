using System;
using System.Collections.Generic;
using Magento.RestClient.Data.Models.EAV.Attributes;

namespace Magento.RestClient.Domain.Models.Catalog
{
	internal class InvalidCustomAttributeException : Exception
	{
		public string AttributeCode { get; }
		public List<Option> ValidOptions { get; }
		public string? InputValue { get; }

		public InvalidCustomAttributeException(string attributeCode, List<Option> attributeOptions, object inputValue) :
			base(
				$"The assigned value of {inputValue} is not valid for {attributeCode}."
			)
		{
			this.AttributeCode = attributeCode;
			this.ValidOptions = attributeOptions;
			this.InputValue = inputValue.ToString();
		}
	}
}