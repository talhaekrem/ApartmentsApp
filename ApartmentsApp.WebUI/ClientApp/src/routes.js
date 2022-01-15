import { Navigate, useRoutes } from 'react-router-dom';
// layouts
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import Login from './pages/Login';
import NotFound from './pages/Page404';
// ----------------------------------------------------------------------
import AdminDashboardLayout from './layouts/dashboard/Admin';
import DashboardApp from './pages/DashboardApp';
import House from './pages/House/House';
import AddHouse from './pages/House/AddHouse';
import UpdateHouse from './pages/House/UpdateHouse';
import DetailHouse from './pages/House/DetailHouse';
// ----------------------------------------------------------------------


// ----------------------------------------------------------------------
import UserDashboardLayout from './layouts/dashboard/User';
import UserDashboard from './pages/UserDashboard';
import User from './pages/User/User';
import AddUser from './pages/User/AddUser';
import UpdateUser from './pages/User/UpdateUser';
import DetailUser from './pages/User/DetailUser';
// ----------------------------------------------------------------------
import Message from './pages/Message/Message';
import AddMessage from './pages/Message/AddMessage';
import DetailMessage from './pages/Message/DetailMessage';
// ----------------------------------------------------------------------
import BillAdmin from './pages/BillAdmin/BillAdmin';
import AddBillAdmin from './pages/BillAdmin/AddBillAdmin';
import DetailBillAdmin from './pages/BillAdmin/DetailBillAdmin';
// ----------------------------------------------------------------------
import BillUser from './pages/BillUser/BillUser';
import DetailBillUser from './pages/BillUser/DetailBillUser';
// ----------------------------------------------------------------------
import AddCard from './pages/Profile/AddCard';
// ----------------------------------------------------------------------

export default function Router() {
    return useRoutes([
        {
            //admin sayfaları
            path: '/admin',
            element: <AdminDashboardLayout />,
            children: [
                { element: <Navigate to="/admin/index" replace /> },
                { path: 'index', element: <DashboardApp /> },
                { path: 'users', element: <User /> },
                { path: 'houses', element: <House /> },
                { path: 'bills', element: <BillAdmin /> },
                { path: 'messages', element: <Message /> },

            ]
        },
        {//ev sayfaları
            path: '/admin/houses',
            element: <AdminDashboardLayout />,
            children: [
                { path: 'add', element: <AddHouse /> },
                { path: 'update/:houseId', element: <UpdateHouse /> },
                { path: 'detail/:houseId', element: <DetailHouse /> },
            ]
        },
        {//fatura sayfaları
            path: '/admin/bills',
            element: <AdminDashboardLayout />,
            children: [
                { path: 'add', element: <AddBillAdmin /> },
                { path: ':type/:billId', element: <DetailBillAdmin /> },
            ]
        },
        {//user sayfaları
            path: '/admin/users',
            element: <AdminDashboardLayout />,
            children: [
                { path: 'add', element: <AddUser /> },
                { path: 'update/:userId', element: <UpdateUser /> },
                { path: 'detail/:userId', element: <DetailUser /> },
            ]
        },
        {//mesaj sayfaları
            path: '/admin/messages',
            element: <AdminDashboardLayout />,
            children: [
                { path: 'add', element: <AddMessage /> },
                { path: 'detail/:messageId', element: <DetailMessage /> },
            ]
        },
        //kullanıcı sayfaları
        {
            path: '/dashboard',
            element: <UserDashboardLayout />,
            children: [
                { element: <Navigate to="/dashboard/index" replace /> },
                { path: 'index', element: <UserDashboard /> },
                { path: 'bills', element: <BillUser /> },
                { path: 'messages', element: <Message /> },
                { path: 'addCard', element: < AddCard/> }
            ]
        },
        {//mesaj sayfaları
            path: '/dashboard/messages',
            element: <UserDashboardLayout />,
            children: [
                { path: 'add', element: <AddMessage /> },
                { path: 'detail/:messageId', element: <DetailMessage /> },
            ]
        },
        {//user faturalar sayfası
            path: '/dashboard/bills',
            element: <UserDashboardLayout />,
            children: [
                { path: 'detail/:type/:billId', element: <DetailBillUser /> },
            ]
        },
        //login register sayfalari
        {
            path: '/auth',
            element: <LogoOnlyLayout />,
            children: [
                { element: <Navigate to="/auth/login" replace /> },
                { path: 'login', element: <Login /> },
            ]
        },
        {
            path: '/',
            element: <LogoOnlyLayout />,
            children: [
                { path: '404', element: <NotFound /> },
                { path: '/', element: <Navigate to="/auth" /> },
                { path: '*', element: <Navigate to="/404" /> }
            ]
        },
        { path: '*', element: <Navigate to="/404" replace /> }
    ]);
}
