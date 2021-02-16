﻿using Microsoft.SharePoint.Client;
using PnP.Core.Model.SharePoint;
using PnP.Core.Services;
using PnP.PowerShell.Commands.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PagePipeBind
    {
        private readonly IPage _page;
        private string _name;

        public PagePipeBind(IPage page)
        {
            _page = page;
            _name = page.PageTitle;
        }

        public PagePipeBind(string name)
        {
            _page = null;
            _name = name;
        }

        public IPage Page => _page;

        public string Name => PageUtilities.EnsureCorrectPageName(_name);

        public override string ToString() => Name;

        internal IPage GetPage()
        {
            var ctx = PnPConnection.CurrentConnection.PnPContext;
            if (_page != null)
            {
                return _page;
            }
            else if (!string.IsNullOrEmpty(_name))
            {
                try
                {
                    var pages = ctx.Web.GetPages(Name);
                    if (pages != null)
                    {
                        var page = pages.FirstOrDefault(p => p.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase));

                        if (page != null)
                        {
                            return page;
                        }
                    }
                    return null;
                }
                catch (ArgumentException)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}