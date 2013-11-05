using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpMessageBus.Server 
{
	public partial class MessageBus 
	{

		//-------------------------------------------------------------------------
		# region Property List<MessageBusChannel> MessageBusChannels w Event post (MessageBusChannelsChanged)
		/// <summary>
		/// MessageBusChannels
		/// </summary>
		public
		  List<MessageBusChannel>
		  MessageBusChannels
		{
			get
			{
				return message_bus_channels;
			} // MessageBusChannels.get
			set
			{
				//if (message_bus_channels != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(message_bus_channels) // MultiThread safe				
					{
						message_bus_channels = value;
						if (null != MessageBusChannelsChanged)
						{
							MessageBusChannelsChanged(this, new EventArgs());
						}
					}
				}
			} // MessageBusChannels.set
		} // MessageBusChannels

		/// <summary>
		/// private member field for holding MessageBusChannels data
		/// </summary>
		private
			List<MessageBusChannel>
			message_bus_channels
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// MessageBusChannelsChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			MessageBusChannelsChanged
			;
		# endregion Property List<MessageBusChannel> MessageBusChannels w Event post (MessageBusChannelsChanged)
		//-------------------------------------------------------------------------

		public MessageBus()
		{
			this.message_bus_channels = new List<MessageBusChannel>();

			return;
		}

	
	}
}
