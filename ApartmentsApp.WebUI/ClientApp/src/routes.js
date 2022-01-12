import { Navigate, useRoutes } from 'react-router-dom';
// layouts
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import Login from './pages/Login';
//import Products from './pages/Products';
import Blog from './pages/Blog';
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
                { path: 'bills', element: <Blog /> },
                { path: 'messages', element: <Blog /> },

            ]
        },
        {//house sayfalari
            path: '/admin/houses',
            element: <AdminDashboardLayout />,
            children: [
                { path: 'add', element: <AddHouse /> },
                { path: 'update/:houseId', element: <UpdateHouse /> },
                { path: 'detail/:houseId', element: <DetailHouse /> },
            ]
        },
        {//user sayfalari
            path: '/admin/users',
            element: <AdminDashboardLayout />,
            children: [
                { path: 'add', element: <AddUser /> },
                { path: 'update/:userId', element: <UpdateUser /> },
                { path: 'detail/:userId', element: <DetailUser /> },
            ]
        },
        //kullanıcı sayfaları
        {
            path: '/dashboard',
            element: <UserDashboardLayout />,
            children: [
                { element: <Navigate to="/dashboard/index" replace /> },
                { path: 'index', element: <UserDashboard /> },
                { path: 'bills', element: <Blog /> },
                { path: 'messages', element: <Blog /> }
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
