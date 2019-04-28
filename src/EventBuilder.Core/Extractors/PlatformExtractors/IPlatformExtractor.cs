// Copyright (c) 2019 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventBuilder.Core.Extractors.PlatformExtractors
{
    /// <summary>
    /// Interface representing a platform assemblies and events.
    /// </summary>
    public interface IPlatformExtractor : IExtractor
    {
        /// <summary>
        /// Gets the event builder platform.
        /// </summary>
        AutoPlatform Platform { get; }

        /// <summary>
        /// Extract details about the platform.
        /// </summary>
        /// <param name="referenceAssembliesLocation">The location for reference assemblies if needed.</param>
        /// <returns>A task to monitor the progress.</returns>
        Task Extract(string referenceAssembliesLocation);
    }
}
