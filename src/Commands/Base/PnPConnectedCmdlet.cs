﻿using System;
using System.Net.Http;

namespace PnP.PowerShell.Commands.Base
{
    /// <summary>
    /// Base class for all the PnP Cmdlets which require a connection to have been made
    /// </summary>
    public abstract class PnPConnectedCmdlet : BasePSCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            // Ensure there is an active connection
            if (PnPConnection.CurrentConnection == null)
            {
                throw new InvalidOperationException(Properties.Resources.NoConnection);
            }
        }

        public HttpClient HttpClient => PnP.Framework.Http.PnPHttpClient.Instance.GetHttpClient();

    }


}
