import { Navigate, useRoutes } from 'react-router-dom';
// layouts
import DashboardLayout from './layouts/dashboard';
import LogoOnlyLayout from './layouts/LogoOnlyLayout';
//
import Login from './pages/Login';
import Register from './pages/Register';
import DashboardApp from './pages/DashboardApp';
//import Products from './pages/Products';
import Blog from './pages/Blog';
import NotFound from './pages/Page404';
// ----------------------------------------------------------------------
import House from './pages/House/House';
import AddHouse from './pages/House/AddHouse';
import UpdateHouse from './pages/House/UpdateHouse';
import DetailHouse from './pages/House/DetailHouse';
// ----------------------------------------------------------------------


// ----------------------------------------------------------------------
import User from './pages/User/User';
import AddUser from './pages/User/AddUser';
import UpdateUser from './pages/User/UpdateUser';
import DetailUser from './pages/User/DetailUser';
// ----------------------------------------------------------------------
export default function Router() {
    return useRoutes([
        {
            //admin sayfalari
            path: '/admin',
            element: <DashboardLayout />,
            children: [
                { element: <Navigate to="/admin/index" replace /> },
                { path: 'index', element: <DashboardApp /> },
                { path: 'users', element: <User /> },
                { path: 'houses', element: <House /> },
                { path: 'bills', element: <Blog /> },
                { path: 'messages', element: <Blog /> },

            ]
        },
        //kullanıcı sayfaları
        {
            path: '/dashboard',
            element: <DashboardLayout />,
            children: [
                { element: <Navigate to="/dashboard/index" replace /> },
                { path: 'index', element: <DashboardApp /> },
                { path: 'bills', element: <Blog /> },
                { path: 'messages', element: <Blog /> }
            ]
        },
        {//house sayfalari
            path: '/admin/houses',
            element: <DashboardLayout />,
            children: [
                { path: 'add', element: <AddHouse /> },
                { path: 'update/:houseId', element: <UpdateHouse /> },
                { path: 'detail/:houseId', element: <DetailHouse /> },
            ]
        },
        {//user sayfalari
            path: '/admin/users',
            element: <DashboardLayout />,
            children: [
                { path: 'add', element: <AddUser /> },
                { path: 'update/:userId', element: <UpdateUser /> },
                { path: 'detail/:userId', element: <DetailUser /> },
            ]
        },
        //login register sayfalari
        {
            path: '/auth',
            element: <LogoOnlyLayout />,
            children: [
                { path: 'login', element: <Login /> },
                { path: 'register', element: <Register /> },
                { path: 'accessdenied', element: <Register /> },
            ]
        },
        {
            path: '/',
            element: <LogoOnlyLayout />,
            children: [
                { path: '404', element: <NotFound /> },
                { path: '/', element: <Navigate to="/dashboard" /> },
                //{ path: '*', element: <Navigate to="/404" /> }
            ]
        },
        { path: '*', element: <Navigate to="/404" replace /> }
    ]);
}
