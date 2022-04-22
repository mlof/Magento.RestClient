using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Magento.Manager.ViewModels
{
	[ObservableObject]
	public partial class MainWindowViewModel
	{
		public MainWindowViewModel()
		{

		}
	}
	[ObservableObject]
	public partial class AttributePageViewModel
	{
		private readonly IAdminContext magentoContext;

		public ObservableCollection<AttributeSetViewModel> AttributeSets { get; set; } =
			new ObservableCollection<AttributeSetViewModel>();
		public AttributePageViewModel()
		{
			
			this.magentoContext = App.Current.Services.GetRequiredService<IAdminContext>();
		}

		[ICommand]
		public async Task Load()
		{

			
			AttributeSets.Clear();
			var attributeSets = magentoContext.AttributeSets.AsQueryable().ToList();
			foreach (var attributeSet in attributeSets)
			{
				var vm = new AttributeSetViewModel(attributeSet) ;
				AttributeSets.Add(vm);
				
			}


		}

	}

	[ObservableObject]
	public partial class AttributeSetViewModel 
	{
		public AttributeSetViewModel(AttributeSet attributeSet)
		{
			this.AttributeSetId = attributeSet.AttributeSetId;
			this.AttributeSetName = attributeSet.AttributeSetName;
			this.SortOrder = attributeSet.SortOrder;
			this.EntityTypeId = attributeSet.EntityTypeId;
		}

		public EntityType EntityTypeId { get; set; }

		public long SortOrder { get; set; }

		public string AttributeSetName { get; set; }

		public long? AttributeSetId { get; set; }
	}
}
