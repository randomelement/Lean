﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using QuantConnect.Data;
using QuantConnect.Data.UniverseSelection;
using QuantConnect.Interfaces;
using QuantConnect.Securities;

namespace QuantConnect.Algorithm.Framework.Selection
{
    /// <summary>
    /// Defines a universe as a set of manually set symbols. This differs from <see cref="UserDefinedUniverse"/>
    /// in that these securities were not added via AddSecurity.
    /// </summary>
    public class ManualUniverse : UserDefinedUniverse
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ManualUniverse"/>
        /// </summary>
        [Obsolete("This constructor is obsolete because SecurityInitializer is obsolete and will not be used.")]
        public ManualUniverse(SubscriptionDataConfig configuration,
            UniverseSettings universeSettings,
            ISecurityInitializer securityInitializer,
            IEnumerable<Symbol> symbols)
            : base(configuration, universeSettings, securityInitializer, Time.MaxTimeSpan, symbols)
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ManualUniverse"/>
        /// </summary>
        public ManualUniverse(SubscriptionDataConfig configuration,
            UniverseSettings universeSettings,
            IEnumerable<Symbol> symbols)
            : base(configuration, universeSettings, Time.MaxTimeSpan, symbols)
        {
        }

        /// <summary>
        /// Gets the subscription requests to be added for the specified security
        /// </summary>
        /// <param name="security">The security to get subscriptions for</param>
        /// <param name="currentTimeUtc">The current time in utc. This is the frontier time of the algorithm</param>
        /// <param name="maximumEndTimeUtc">The max end time</param>
        /// <param name="subscriptionService">Instance which implements <see cref="ISubscriptionDataConfigService"/> interface</param>
        /// <returns>All subscriptions required by this security</returns>
        public override IEnumerable<SubscriptionRequest> GetSubscriptionRequests(Security security, DateTime currentTimeUtc, DateTime maximumEndTimeUtc,
                                                                                ISubscriptionDataConfigService subscriptionService)
        {
            return security.Subscriptions.Select(config =>
                new SubscriptionRequest(
                    isUniverseSubscription: false,
                    universe: this,
                    security: security,
                    configuration: new SubscriptionDataConfig(config, isInternalFeed: false),
                    startTimeUtc: currentTimeUtc,
                    endTimeUtc: maximumEndTimeUtc
                )
            );
        }
    }
}