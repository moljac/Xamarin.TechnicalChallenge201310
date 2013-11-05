using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HttpMessageBus.Server 
{
	public partial class MessageBusChannel 
	{

		//-------------------------------------------------------------------------
		# region Property string Channel w Event post (ChannelChanged)
		/// <summary>
		/// Channel
		/// </summary>
		public
		  string
		  Channel
		{
			get
			{
				return channel;
			} // Channel.get
			set
			{
				//if (channel != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(channel) // MultiThread safe				
					{
						channel = value;
						if (null != ChannelChanged)
						{
							ChannelChanged(this, new EventArgs());
						}
					}
				}
			} // Channel.set
		} // Channel

		/// <summary>
		/// private member field for holding Channel data
		/// </summary>
		private
			string
			channel
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// ChannelChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			ChannelChanged
			;
		# endregion Property string Channel w Event post (ChannelChanged)
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		# region Property List<string> UsersClients w Event post (UsersClientsChanged)
		/// <summary>
		/// UsersClients
		/// </summary>
		public
		  List<string>
		  UsersClients
		{
			get
			{
				return users_clients;
			} // UsersClients.get
			set
			{
				//if (users_clients != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(users_clients) // MultiThread safe				
					{
						users_clients = value;
						if (null != UsersClientsChanged)
						{
							UsersClientsChanged(this, new EventArgs());
						}
					}
				}
			} // UsersClients.set
		} // UsersClients

		/// <summary>
		/// private member field for holding UsersClients data
		/// </summary>
		private
			List<string>
			users_clients
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// UsersClientsChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			UsersClientsChanged
			;
		# endregion Property List<string> UsersClients w Event post (UsersClientsChanged)
		//-------------------------------------------------------------------------

		//-------------------------------------------------------------------------
		# region Property List<HttpListenerContext> UserContexts w Event post (UserContextsChanged)
		/// <summary>
		/// UserContexts
		/// </summary>
		[System.Xml.Serialization.XmlIgnore]
		public
		  List<HttpListenerContext>
		  UserContexts
		{
			get
			{
				return user_contexts;
			} // UserContexts.get
			set
			{
				//if (user_contexts != value)		// do not write if equivalent/equal/same
				{
					// for multi threading apps uncomment lines beginnig with //MT:
					//MT: lock(user_contexts) // MultiThread safe				
					{
						user_contexts = value;
						if (null != UserContextsChanged)
						{
							UserContextsChanged(this, new EventArgs());
						}
					}
				}
			} // UserContexts.set
		} // UserContexts

		/// <summary>
		/// private member field for holding UserContexts data
		/// </summary>
		private
			List<HttpListenerContext>
			user_contexts
			;

		///<summary>
		/// Event for wiring BusinessLogic object changes and presentation
		/// layer notifications.
		/// UserContextsChanged (<propertyname>Changed) is intercepted by Windows Forms
		/// 1.x and 2.0 event dispatcher 
		/// and for some CLR types by WPF event dispatcher 
		/// usually INotifyPropertyChanged and PropertyChanged event
		///</summary>
		public
			event
			EventHandler
			UserContextsChanged
			;
		# endregion Property List<HttpListenerContext> UserContexts w Event post (UserContextsChanged)
		//-------------------------------------------------------------------------
		
		public MessageBusChannel()
		{
			this.users_clients =  new List<string>();
			this.user_contexts = new List<HttpListenerContext>();

			return;
		}	
	}
}
