import React from "react";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { motion, AnimatePresence } from "framer-motion";
import { api, setAuthToken } from "../../lib/AuthCall";
import "./AuthForm.css";

// ===== Schemas =====
const loginSchema = z.object({
  email: z.string().email("Please enter a valid email"),
  password: z.string().min(8, "Password must be at least 8 characters"),
  remember: z.boolean().default(false),
});

const registerSchema = z.object({
  email: z.string().email("Please enter a valid email"),
  username: z
    .string()
    .min(3, "Username must be at least 3 characters")
    .max(24, "Username must be at most 24 characters")
    .regex(/^[a-zA-Z0-9_]+$/, "Only letters, numbers and underscores"),
  password: z
    .string()
    .min(8, "Password must be at least 8 characters")
    .refine((val) => /[A-Z]/.test(val), { message: "Add at least one uppercase letter" })
    .refine((val) => /[a-z]/.test(val), { message: "Add at least one lowercase letter" })
    .refine((val) => /\d/.test(val), { message: "Add at least one number" }),
});

type LoginFormInput = z.input<typeof loginSchema>;
type RegisterFormInput = z.input<typeof registerSchema>;

function classNames(...cn: Array<string | false | null | undefined>) {
  return cn.filter(Boolean).join(" ");
}

function scorePassword(pw: string) {
  let score = 0;
  if (!pw) return score;
  const variations = {
    digits: /\d/.test(pw),
    lower: /[a-z]/.test(pw),
    upper: /[A-Z]/.test(pw),
    nonWords: /[^a-zA-Z0-9]/.test(pw),
  };
  score += Math.min(10, Math.floor(pw.length * 1.5));
  score += Object.values(variations).filter(Boolean).length * 10;
  return Math.min(score, 40);
}

// ===== Component =====
const AuthForms: React.FC = () => {
  const [tab, setTab] = React.useState<"login" | "register">("login");
  const [showLoginPassword, setShowLoginPassword] = React.useState(false);
  const [showRegisterPassword, setShowRegisterPassword] = React.useState(false);
  const [isLoading, setIsLoading] = React.useState(false);
  const [error, setError] = React.useState<string | null>(null);

  // Login form
  const {
    register: loginRegister,
    handleSubmit: handleLoginSubmit,
    formState: { errors: loginErrors, isSubmitting: loginSubmitting },
  } = useForm<LoginFormInput>({
    resolver: zodResolver(loginSchema),
    defaultValues: { email: "", password: "", remember: false },
    mode: "onTouched",
  });

  // Register form
  const {
    register: registerRegister,
    handleSubmit: handleRegisterSubmit,
    watch: registerWatch,
    formState: { errors: registerErrors, isSubmitting: registerSubmitting },
  } = useForm<RegisterFormInput>({
    resolver: zodResolver(registerSchema),
    defaultValues: { email: "", username: "", password: "" },
    mode: "onTouched",
  });

  const regPw = registerWatch("password");
  const pwPct = (scorePassword(regPw || "") / 40) * 100;
  const busy = isLoading || loginSubmitting || registerSubmitting;

  // ===== Handlers wired to .NET API using axios `api` and `setAuthToken` =====
  const onLogin = async (values: LoginFormInput) => {
    const data = loginSchema.parse(values);
    try {
      setError(null);
      setIsLoading(true);
      // Adjust path if your API differs (e.g., "/api/auth/login")
      const res = await api.post<{ token: string; user?: unknown }>("/auth/login", {
        email: data.email,
        password: data.password,
      });

      // Persist token & attach to axios
      setAuthToken(res.data.token, data.remember);

      // TODO: navigate or fetch initial data here
      // e.g., navigate("/dashboard");
    } catch (err: any) {
      const msg =
        err?.response?.data?.message ||
        err?.response?.statusText ||
        err?.message ||
        "Login failed";
      setError(msg);
    } finally {
      setIsLoading(false);
    }
  };

  const onRegister = async (values: RegisterFormInput) => {
    const data = registerSchema.parse(values);
    try {
      setError(null);
      setIsLoading(true);
      // Adjust path if your API differs (e.g., "/api/auth/register")
      await api.post("/auth/register", {
        email: data.email,
        username: data.username,
        password: data.password,
      });

      // Optional: auto-login right after register
      const res = await api.post<{ token: string }>("/auth/login", {
        email: data.email,
        password: data.password,
      });
      setAuthToken(res.data.token, true);
      // TODO: navigate after auto-login
    } catch (err: any) {
      const msg =
        err?.response?.data?.message ||
        err?.response?.statusText ||
        err?.message ||
        "Registration failed";
      setError(msg);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="min-h-screen w-full bg-gradient-to-br from-slate-50 via-white to-slate-100 flex items-center justify-center p-4">
      <motion.div
        initial={{ opacity: 0, y: 12 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ duration: 0.4 }}
        className="w-full max-w-md"
      >
        <div className="backdrop-blur-sm bg-white/80 border border-slate-200 shadow-xl rounded-2xl overflow-hidden">
          {/* Tabs */}
          <div className="grid grid-cols-2">
            <button
              type="button"
              onClick={() => setTab("login")}
              className={classNames(
                "text-center text-sm font-medium py-3 transition",
                tab === "login" ? "bg-white" : "bg-slate-50 hover:bg-slate-100"
              )}
              aria-selected={tab === "login"}
              role="tab"
            >
              Log in
            </button>
            <button
              type="button"
              onClick={() => setTab("register")}
              className={classNames(
                "text-center text-sm font-medium py-3 transition",
                tab === "register" ? "bg-white" : "bg-slate-50 hover:bg-slate-100"
              )}
              aria-selected={tab === "register"}
              role="tab"
            >
              Register
            </button>
          </div>

          <div className="p-6">
            {error && (
              <div className="mb-4 rounded-lg border border-red-200 bg-red-50 text-red-700 text-sm p-3">
                {error}
              </div>
            )}

            <AnimatePresence mode="wait">
              {tab === "login" && (
                <motion.form
                  key="login"
                  initial={{ opacity: 0, x: -8 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, x: 8 }}
                  transition={{ duration: 0.2 }}
                  onSubmit={handleLoginSubmit(onLogin)}
                  noValidate
                  aria-label="Log in form"
                >
                  <div className="space-y-4">
                    <div>
                      <label htmlFor="login-email" className="block text-sm font-medium text-slate-700">
                        Email
                      </label>
                      <input
                        id="login-email"
                        type="email"
                        autoComplete="email"
                        {...loginRegister("email")}
                        className={classNames(
                          "mt-1 block w-full rounded-xl border px-3 py-2 text-sm outline-none transition",
                          "border-slate-300 focus:ring-4 focus:ring-slate-200 focus:border-slate-500",
                          loginErrors.email && "border-red-300 focus:ring-red-100 focus:border-red-500"
                        )}
                        placeholder="you@example.com"
                      />
                      {loginErrors.email && (
                        <p className="mt-1 text-xs text-red-600">{loginErrors.email.message}</p>
                      )}
                    </div>

                    <div>
                      <label htmlFor="login-password" className="block text-sm font-medium text-slate-700">
                        Password
                      </label>
                      <div className="relative mt-1">
                        <input
                          id="login-password"
                          type={showLoginPassword ? "text" : "password"}
                          autoComplete="current-password"
                          {...loginRegister("password")}
                          className={classNames(
                            "block w-full rounded-xl border px-3 py-2 pr-10 text-sm outline-none transition",
                            "border-slate-300 focus:ring-4 focus:ring-slate-200 focus:border-slate-500",
                            loginErrors.password && "border-red-300 focus:ring-red-100 focus:border-red-500"
                          )}
                          placeholder="••••••••"
                        />
                        <button
                          type="button"
                          onClick={() => setShowLoginPassword((s) => !s)}
                          className="absolute inset-y-0 right-2 my-auto h-8 px-2 rounded-lg text-xs text-slate-600 hover:bg-slate-100"
                          aria-label={showLoginPassword ? "Hide password" : "Show password"}
                        >
                          {showLoginPassword ? "Hide" : "Show"}
                        </button>
                      </div>
                      {loginErrors.password && (
                        <p className="mt-1 text-xs text-red-600">{loginErrors.password.message}</p>
                      )}
                    </div>

                    <div className="flex items-center justify-between">
                      <label className="inline-flex items-center gap-2 text-sm text-slate-600">
                        <input type="checkbox" className="h-4 w-4 rounded border-slate-300" {...loginRegister("remember")} />
                        Remember me
                      </label>
                      <button
                        type="button"
                        className="text-sm text-slate-600 hover:text-slate-900 underline underline-offset-2"
                        onClick={() => alert("Implement 'Forgot password' navigation in your app.")}
                      >
                        Forgot password?
                      </button>
                    </div>

                    <button
                      type="submit"
                      disabled={busy}
                      className={classNames(
                        "w-full rounded-xl bg-slate-900 text-white py-2.5 text-sm font-medium shadow-sm",
                        "hover:bg-slate-800 focus:outline-none focus:ring-4 focus:ring-slate-300",
                        busy && "opacity-70 cursor-not-allowed"
                      )}
                    >
                      {busy ? "Signing in…" : "Sign in"}
                    </button>
                  </div>
                </motion.form>
              )}

              {tab === "register" && (
                <motion.form
                  key="register"
                  initial={{ opacity: 0, x: 8 }}
                  animate={{ opacity: 1, x: 0 }}
                  exit={{ opacity: 0, x: -8 }}
                  transition={{ duration: 0.2 }}
                  onSubmit={handleRegisterSubmit(onRegister)}
                  noValidate
                  aria-label="Register form"
                >
                  <div className="space-y-4">
                    <div>
                      <label htmlFor="reg-email" className="block text-sm font-medium text-slate-700">
                        Email
                      </label>
                      <input
                        id="reg-email"
                        type="email"
                        autoComplete="email"
                        {...registerRegister("email")}
                        className={classNames(
                          "mt-1 block w-full rounded-xl border px-3 py-2 text-sm outline-none transition",
                          "border-slate-300 focus:ring-4 focus:ring-slate-200 focus:border-slate-500",
                          registerErrors.email && "border-red-300 focus:ring-red-100 focus:border-red-500"
                        )}
                        placeholder="you@example.com"
                      />
                      {registerErrors.email && (
                        <p className="mt-1 text-xs text-red-600">{registerErrors.email.message}</p>
                      )}
                    </div>

                    <div>
                      <label htmlFor="reg-username" className="block text-sm font-medium text-slate-700">
                        Username
                      </label>
                      <input
                        id="reg-username"
                        type="text"
                        autoComplete="username"
                        {...registerRegister("username")}
                        className={classNames(
                          "mt-1 block w-full rounded-xl border px-3 py-2 text-sm outline-none transition",
                          "border-slate-300 focus:ring-4 focus:ring-slate-200 focus:border-slate-500",
                          registerErrors.username && "border-red-300 focus:ring-red-100 focus:border-red-500"
                        )}
                        placeholder="your_handle"
                      />
                      {registerErrors.username && (
                        <p className="mt-1 text-xs text-red-600">{registerErrors.username.message}</p>
                      )}
                    </div>

                    <div>
                      <label htmlFor="reg-password" className="block text-sm font-medium text-slate-700">
                        Password
                      </label>
                      <div className="relative mt-1">
                        <input
                          id="reg-password"
                          type={showRegisterPassword ? "text" : "password"}
                          autoComplete="new-password"
                          {...registerRegister("password")}
                          className={classNames(
                            "block w-full rounded-xl border px-3 py-2 pr-10 text-sm outline-none transition",
                            "border-slate-300 focus:ring-4 focus:ring-slate-200 focus:border-slate-500",
                            registerErrors.password && "border-red-300 focus:ring-red-100 focus:border-red-500"
                          )}
                          placeholder="At least 8 characters"
                        />
                        <button
                          type="button"
                          onClick={() => setShowRegisterPassword((s) => !s)}
                          className="absolute inset-y-0 right-2 my-auto h-8 px-2 rounded-lg text-xs text-slate-600 hover:bg-slate-100"
                          aria-label={showRegisterPassword ? "Hide password" : "Show password"}
                        >
                          {showRegisterPassword ? "Hide" : "Show"}
                        </button>
                      </div>
                      {registerErrors.password ? (
                        <p className="mt-1 text-xs text-red-600">{registerErrors.password.message}</p>
                      ) : (
                        <div className="mt-2">
                          <div className="h-2 w-full bg-slate-200 rounded-full overflow-hidden">
                            <div
                              className={classNames(
                                "h-full rounded-full transition-all",
                                pwPct < 40 && "bg-red-400",
                                pwPct >= 40 && pwPct < 70 && "bg-yellow-400",
                                pwPct >= 70 && "bg-green-500"
                              )}
                              style={{ width: `${pwPct}%` }}
                              aria-hidden
                            />
                          </div>
                          <p className="mt-1 text-xs text-slate-500">
                            {pwPct < 40 ? "Weak" : pwPct < 70 ? "Okay" : "Strong"}
                          </p>
                        </div>
                      )}
                    </div>

                    <button
                      type="submit"
                      disabled={busy}
                      className={classNames(
                        "w-full rounded-xl bg-slate-900 text-white py-2.5 text-sm font-medium shadow-sm",
                        "hover:bg-slate-800 focus:outline-none focus:ring-4 focus:ring-slate-300",
                        busy && "opacity-70 cursor-not-allowed"
                      )}
                    >
                      {busy ? "Creating account…" : "Create account"}
                    </button>
                  </div>
                </motion.form>
              )}
            </AnimatePresence>

            <div className="mt-6 flex items-center gap-3">
              <div className="h-px flex-1 bg-slate-200" />
              <span className="text-xs text-slate-500">or</span>
              <div className="h-px flex-1 bg-slate-200" />
            </div>

            <div className="mt-4 grid grid-cols-1 gap-3">
              <button
                type="button"
                onClick={() => alert("Hook up OAuth provider in your app")}
                className="w-full rounded-xl border border-slate-300 bg-white px-3 py-2 text-sm font-medium text-slate-700 hover:bg-slate-50"
              >
                Continue with Google
              </button>
            </div>
          </div>
        </div>
      </motion.div>
    </div>
  );
};

export default AuthForms;
