using Convey.CQRS.Commands;
using Convey.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Pacco.Services.Availability.Infrastructure.Decorators
{
    internal sealed class SecurityDecorator<T> : ICommandHandler<T> where T : class, ICommand
    {
        private readonly IHasher _hasher;
        private readonly IEncryptor _encryptor;
        private readonly ISigner _signer;
        private readonly ICommandHandler<T> _commandHandler;

        public SecurityDecorator(ICommandHandler<T> commandHandler, IHasher hasher, IEncryptor encryptor, ISigner signer)
        {
            _hasher = hasher;
            _encryptor = encryptor;
            _signer = signer;
            _commandHandler = commandHandler;
        }

        public Task HandleAsync(T command)
        {
            var text = "keklol";
            var hash = _hasher.Hash(text);

            var secretKey = "#W!Fjk!85PS&$dNoW&7JY^drpU%ThRiG";
            var encryptedText = _encryptor.Encrypt(text, secretKey);
            var decryptedText = _encryptor.Decrypt(encryptedText, secretKey);

            var privKey = new X509Certificate2("certs/localhost.pfx", "test");
            var data = "hohohohoh";

            var signature = _signer.Sign(data, privKey);
            var pubKey = new X509Certificate2("certs/localhost.cer");
            var isValid = _signer.Verify(data, pubKey, signature);

            return _commandHandler.HandleAsync(command);
        }
    }
}
