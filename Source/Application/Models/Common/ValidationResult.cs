namespace Source.Application.Models.Common
{
    public class ValidationResult
    {
        private List<string> _errors = [];
        public IReadOnlyList<string> Errors => _errors.AsReadOnly();
        public bool IsValid => _errors.Count == 0;

        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}