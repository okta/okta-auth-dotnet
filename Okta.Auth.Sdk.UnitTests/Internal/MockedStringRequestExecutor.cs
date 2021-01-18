﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Okta.Sdk.Abstractions;

namespace Okta.Auth.Sdk.UnitTests.Internal
{
    public class MockedStringRequestExecutor : IRequestExecutor
    {
        private readonly string _returnThis;
        private readonly int _statusCode;
        private readonly KeyValuePair<string, IEnumerable<string>>[] _headers;

        public string OktaDomain => throw new NotImplementedException();

        public MockedStringRequestExecutor(string returnThis, int statusCode = 200)
        {
            _headers = new[] { new KeyValuePair<string, IEnumerable<string>> ( "Content-Type", new[] { "application/json" } ) }; 
            _returnThis = returnThis;
            _statusCode = statusCode;
        }

        public Task<HttpResponse<string>> GetAsync(string href, IEnumerable<KeyValuePair<string, string>> headers, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponse<string>
            {
                Headers = _headers,
                StatusCode = _statusCode,
                Payload = _returnThis,
            });

        public Task<HttpResponse<string>> PostAsync(string href, IEnumerable<KeyValuePair<string, string>> headers, string body, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponse<string>
            {
                Headers = _headers,
                StatusCode = _statusCode,
                Payload = _returnThis,
            });

        public Task<HttpResponse<string>> PutAsync(string href, IEnumerable<KeyValuePair<string, string>> headers, string body, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponse<string>> DeleteAsync(string href, IEnumerable<KeyValuePair<string, string>> headers, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
