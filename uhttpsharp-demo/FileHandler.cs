﻿/*
 * Copyright (C) 2011 uhttpsharp project - http://github.com/raistlinthewiz/uhttpsharp
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.

 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */

using System.IO;
using uhttpsharp.Embedded;

namespace uhttpsharpdemo
{
    [HttpRequestHandlerAttributes("*")]
    public class FileHandler : HttpRequestHandler
    {
        public static string HttpRootDirectory { get; set; }

        public override HttpResponse Handle(HttpRequest httpRequest)
        {
            var httpRoot = Path.GetFullPath(HttpRootDirectory ?? ".");
            var requestPath = httpRequest.Uri.AbsolutePath.TrimStart('/');
            var path = Path.GetFullPath(Path.Combine(httpRoot, requestPath));
            if (!File.Exists(path))
                return null;
            return new HttpResponse(HttpResponse.ResponseCode.Ok, File.ReadAllText(path));
        }
    }
}