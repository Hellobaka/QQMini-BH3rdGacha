using SqlSugar;

namespace SQLHelper.Models
{
    /// <summary>
    /// �����û��ֿ����ݵı�
    /// </summary>
    public class Repositories
    {
        /// <summary>
        /// QQ������Ⱥ��
        /// </summary>
        public long fromgroup { get; set; }
        /// <summary>
        /// �û�QQ��
        /// </summary>
        public long qq { get; set; }
        /// <summary>
        /// ��Ŀ���ͣ�������ʥ�۵ȣ�
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// ��ɫ�׼�
        /// </summary>
        public string class_ { get; set; }
        /// <summary>
        /// ��Ŀ�Ǽ�
        /// </summary>
        public long? level { get; set; }
        /// <summary>
        /// ��Ŀ��ֵ
        /// </summary>
        public long? value { get; set; }
        /// <summary>
        /// ��ɫ��������ɫ
        /// </summary>
        public long? quality { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public long? count { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        public string date { get; set; }
    }
}