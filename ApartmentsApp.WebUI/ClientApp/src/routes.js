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
import User from './pages/User';
import NotFound from './pages/Page404';
// ----------------------------------------------------------------------
import House from './pages/House/House';
import AddHouse from './pages/House/AddHouse';
import UpdateHouse from './pages/House/UpdateHouse';
import DetailHouse from './pages/House/DetailHouse';
// ----------------------------------------------------------------------

export default function Router() {
    return useRoutes([
        {
            //admin sayfalar�
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
            path: '/home',
            element: <DashboardLayout />,
            children: [
                { element: <Navigate to="/home/index" replace /> },
                { path: 'index', element: <DashboardApp /> },
                { path: 'bills', element: <Blog /> },
                { path: 'messages', element: <Blog /> }
            ]
        },
        {
            path: '/admin/houses',
            element: <DashboardLayout />,
            children: [
                { path: 'add', element: <AddHouse /> },
                { path: 'update/:houseId', element: <UpdateHouse /> },
                { path: 'detail/:houseId', element: <DetailHouse /> },
            ]
        },
        //login register sayfalar�
        {
            path: '/account',
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
                { path: '/', element: <Navigate to="/home" /> },
                { path: '*', element: <Navigate to="/404" /> }
            ]
        },
        { path: '*', element: <Navigate to="/404" replace /> }
    ]);
}
