// Example: Simple auth check (replace with your real auth logic)
const isAuthenticated = () => {
  // For example, check if a token exists in localStorage
  return !!localStorage.getItem("authToken");
};

// ProtectedRoute component
const ProtectedRoute = ({ children }) => {
  return isAuthenticated() ? children : <Navigate to="/login" replace />;
};



 <Route
              path="/dashboard"
              element={
                <ProtectedRoute>
                  <Dashboard />
                </ProtectedRoute>
              }
            />