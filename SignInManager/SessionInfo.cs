using System;

namespace mYSelfERPWeb
{
    public class SessionInfo
    {
        private string _userId;
        private string _userName;
        private string _roleName;
        private int _roleId;
        private string _companyCode;
        private string _sisterConcernCode;
        private string _companyName;
        private string _companyAddress;
        private string _sisterConcernName;
        private string _sisterConcernAddress;
        private byte[] _companyLogo;
        private byte[] _sisterConcernLogo;

        public SessionInfo()
        {
        }

        public string UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Role
        {
            get { return _roleName; }
            set { _roleName = value; }
        }

        public int RoleId
        {
            get { return _roleId; }
            set { _roleId = value; }
        }

        public string CompanyCode
        {
            get { return _companyCode; }
            set { _companyCode = value; }
        }

        public string SisterConcernCode
        {
            get { return _sisterConcernCode; }
            set { _sisterConcernCode = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string CompanyAddress
        {
            get { return _companyAddress; }
            set { _companyAddress = value; }
        }

        public string SisterConcernName
        {
            get { return _sisterConcernName; }
            set { _sisterConcernName = value; }
        }

        public string SisterConcernAddress
        {
            get { return _sisterConcernAddress; }
            set { _sisterConcernAddress = value; }
        }

        public byte[] CompanyLogo
        {
            get { return _companyLogo; }
            set { _companyLogo = value; }
        }

        public byte[] SisterConcernLogo
        {
            get { return _sisterConcernLogo; }
            set { _sisterConcernLogo = value; }
        }
    }
}
