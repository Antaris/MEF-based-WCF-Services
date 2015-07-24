namespace FD.ServiceHost.Hosting
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition.Hosting;
	using System.Configuration;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Configuration;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;
	using System.Reflection;

	/// <summary>
	/// Providers instance creation through a composition container.
	/// </summary>
	public static class BehaviorFactory
	{
		private static readonly MethodInfo CreateBehaviorMethod = typeof(BehaviorExtensionElement).GetMethod("CreateBehavior", BindingFlags.Instance | BindingFlags.NonPublic);

		/// <summary>
		/// Gets the configured behaviors for the given service.
		/// </summary>
		/// <param name="serviceName">The service name.</param>
		/// <returns>The set of configured services.</returns>
		public static IEnumerable<IServiceBehavior> GetConfiguredBehaviors(string serviceName)
		{
			if (string.IsNullOrWhiteSpace(serviceName))
			{
				throw new ArgumentException("The serviceName parameter is required.");
			}

			var result = new List<IServiceBehavior>();

			// Get the config.
			var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			var services = config.GetSection("system.serviceModel/services") as ServicesSection;

			if (services != null)
			{
				var service = services.Services.OfType<ServiceElement>().SingleOrDefault(s => string.Equals(s.Name, serviceName, StringComparison.OrdinalIgnoreCase));
				if (service != null && !string.IsNullOrWhiteSpace(service.BehaviorConfiguration))
				{
					// We've got a behavior configuration, so discover that.
					var behaviors = config.GetSection("system.serviceModel/behaviors") as BehaviorsSection;
					if (behaviors != null && behaviors.ServiceBehaviors != null && behaviors.ServiceBehaviors.Count > 0)
					{
						foreach (ServiceBehaviorElement sbe in behaviors.ServiceBehaviors)
						{
							foreach (BehaviorExtensionElement bxe in sbe)
							{
								result.Add((IServiceBehavior)CreateBehaviorMethod.Invoke(bxe, new object[0]));
							}
						}
					}
				}
			}

			return result;
		}
	}
}