using BookShop2023.DAO;
using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop2023.BUS
{
    internal class VoucherBUS
    {
        VoucherDAO _voucherDAO;

        public VoucherBUS()
        {
            if (null == _voucherDAO)
            {
                _voucherDAO = new VoucherDAO();
                if (_voucherDAO.CanConnect())
                {
                    _voucherDAO.Connect();
                }
            }
        }

        public Voucher GetVoucherById(int id)
        {
            Voucher result = _voucherDAO.GetVoucherById(id);

            return result;
        }

        public List<Voucher> GetAllVouchers()
        {
            return _voucherDAO.getVoucherList();
        }
        public void AddVoucher(Voucher v)
        {
            
            
                // add new voucher
                _voucherDAO.AddVoucher(v);
                v.ID = _voucherDAO.GetLastestInsertID();
            
        }

        public void removeVoucher(Voucher cat)
        {
            _voucherDAO.removeVoucher(cat.ID);
        }

        public void updateVoucher(int ID, Voucher voucher)
        {

            _voucherDAO.updateVoucher(ID, voucher);

        }
    }
}
