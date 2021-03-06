using System;
using System.Collections.Generic;
using System.Net;
using WindowsFirewallHelper.Addresses;

namespace WindowsFirewallHelper.InternalHelpers
{
    // ReSharper disable once HollowTypeName
    internal static class AddressHelper
    {
        public static string AddressesToString(IAddress[] rules)
        {
            var addresses = new List<string>();

            foreach (var address in rules)
            {
                if (SingleIP.Any.Equals(address))
                {
                    addresses.Clear();
                    addresses.Add(address.ToString());

                    break;
                }

                addresses.Add(address.ToString());
            }

            return string.Join(",", addresses.ToArray());
        }

        // ReSharper disable once MethodNameNotMeaningful
        public static IPAddress Max(IPAddress val1, IPAddress val2)
        {
            if (val1.AddressFamily != val2.AddressFamily)
            {
                throw new ArgumentException("Addresses of different family can not be compared.");
            }

            var bytes1 = val1.GetAddressBytes();
            var bytes2 = val2.GetAddressBytes();

            for (var i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] > bytes2[i])
                {
                    return val1;
                }

                if (bytes2[i] > bytes1[i])
                {
                    return val2;
                }
            }

            return val1;
        }

        // ReSharper disable once MethodNameNotMeaningful
        public static IPAddress Min(IPAddress val1, IPAddress val2)
        {
            if (val1.AddressFamily != val2.AddressFamily)
            {
                throw new ArgumentException("Addresses of different family can not be compared.");
            }

            var bytes1 = val1.GetAddressBytes();
            var bytes2 = val2.GetAddressBytes();

            for (var i = 0; i < bytes1.Length; i++)
            {
                if (bytes1[i] < bytes2[i])
                {
                    return val1;
                }

                if (bytes2[i] < bytes1[i])
                {
                    return val2;
                }
            }

            return val1;
        }


        // ReSharper disable once ExcessiveIndentation
        public static IAddress[] StringToAddresses(string str)
        {
            var remoteAddresses = new List<IAddress>();

            foreach (var remoteAddress in str.Split(','))
            {
                if (DNSService.TryParse(remoteAddress, out var dns))
                {
                    remoteAddresses.Add(dns);
                }
                else if (DHCPService.TryParse(remoteAddress, out var dhcp))
                {
                    remoteAddresses.Add(dhcp);
                }
                else if (WINSService.TryParse(remoteAddress, out var wins))
                {
                    remoteAddresses.Add(wins);
                }
                else if (LocalSubnet.TryParse(remoteAddress, out var localSubnet))
                {
                    remoteAddresses.Add(localSubnet);
                }
                else if (DefaultGateway.TryParse(remoteAddress, out var defaultGateway))
                {
                    remoteAddresses.Add(defaultGateway);
                }
                else if (IPRange.TryParse(remoteAddress, out var range))
                {
                    remoteAddresses.Add(range);
                }
                else if (SingleIP.TryParse(remoteAddress, out SingleIP ip))
                {
                    remoteAddresses.Add(ip);
                }
                else if (NetworkAddress.TryParse(remoteAddress, out var network))
                {
                    remoteAddresses.Add(network);
                }
            }

            return remoteAddresses.ToArray();
        }
    }
}